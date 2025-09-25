using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using Interface.Model.Global;
using MessagePack;
using Module.Option.Runtime;
using Module.Repository;
using Repository;
using Structure.Global;
using UnityEngine;

namespace Model.Global.SaveData
{
    public class GameStateModel : IGameStateModel, IRepositoryFlushModel
    {
        public GameStateModel
        (
            IRepositoryWriter<UserStateDto> userStateWriter,
            IRepositoryReader<UserStateDto> userStateReader
        )
        {
            UserStateWriter = userStateWriter;
            UserStateReader = userStateReader;
            _userState = Option<UserState>.None();
        }

        private const string UserStateFile = "UserState.sav";

        public async UniTask Initialize()
        {
            var result = await UserStateReader.Read(UserStateFile);

            if (result.TryGetValue(out var dto))
            {
                _userState = Option<UserState>.Some(dto.Convert());
            }
            else
            {
                _userState = Option<UserState>.Some(new UserState(
                    GameState.Tutorial,
                    string.Empty,
                    new Dictionary<string, StageProgressData>()
                ));
            }
        }

        public void UpdateGameState(GameState gameState)
        {
            Debug.Assert(_userState.IsNone);

            _userState = Option<UserState>.Some(new UserState(
                gameState,
                ClearedStageName,
                ProgressData
            ));
        }

        public UniTask Flush()
        {
            Debug.Log($"Flushed\n{_userState}");
            return UserStateWriter.Write(UserStateFile, _userState.Unwrap().Convert());
        }

        // 初期化前にアクセスしたらエラー
        public GameState GameState => _userState.Unwrap().GameState;
        public string ClearedStageName => _userState.Unwrap().ClearedStageName;
        public Dictionary<string, StageProgressData> ProgressData => _userState.Unwrap().ProgressData;

        private Option<UserState> _userState;

        private IRepositoryWriter<UserStateDto> UserStateWriter { get; }
        private IRepositoryReader<UserStateDto> UserStateReader { get; }
    }

    public class UserStateRepository : MessagePackRepository<UserStateDto>
    {
        public override UniTask Write(string fileName, UserStateDto dataTransferObject)
        {
            return InnerWrite(fileName, dataTransferObject);
        }

        public override UniTask<Option<UserStateDto>> Read(string fileName)
        {
            return InnerRead(fileName);
        }
    }


    /// <summary>
    /// ステージ中断時に生成されるセーブデータ
    /// </summary>
    [MessagePackObject]
    public struct StageDto
    {
        [Key(0)] public Vector3 PlayerPosition;
        [Key(1)] public Quaternion PlayerRotation;
        [Key(2)] public (Vector3, Quaternion)[] KickPositions;

        public StageDto
        (
            Vector3 playerPosition,
            Quaternion playerRotation,
            (Vector3, Quaternion)[] kickPositions
        )
        {
            PlayerPosition = playerPosition;
            PlayerRotation = playerRotation;
            KickPositions = kickPositions;
        }
    }

    public static class Extension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UserStateDto Convert(this UserState state)
        {
            return new UserStateDto(state.GameState, state.ClearedStageName, state.ProgressData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UserState Convert(this UserStateDto state)
        {
            return new UserState(state.GameState, state.ClearedStageName, state.ProgressData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StageDto Convert(this StageData state)
        {
            return new StageDto(state.PlayerPosition, state.PlayerRotation, state.KickPositions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StageData Convert(this StageDto state)
        {
            return new StageData(state.PlayerPosition, state.PlayerRotation, state.KickPositions);
        }
    }
}