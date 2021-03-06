using Unity.AI.Planner;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.DomainLanguage.TraitBased;

namespace AI.Planner.Domains
{
    public struct SearchPruned
    {
        public bool IsTerminal(StateData stateData)
        {
            var GameFilter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Game>(),  };
            var GameObjectIndices = new NativeList<int>(2, Allocator.Temp);
            stateData.GetTraitBasedObjectIndices(GameObjectIndices, GameFilter);
            var GameBuffer = stateData.GameBuffer;
            
            for (int i0 = 0; i0 < GameObjectIndices.Length; i0++)
            {
                var GameIndex = GameObjectIndices[i0];
                var GameObject = stateData.TraitBasedObjects[GameIndex];
                
                if (!(GameBuffer[GameObject.GameIndex].Score < 0))
                    continue;
                GameFilter.Dispose();
                return true;
            }
            GameObjectIndices.Dispose();
            GameFilter.Dispose();

            return false;
        }

        public float TerminalReward(StateData stateData)
        {
            var reward = 0f;

            return reward;
        }
    }
}
