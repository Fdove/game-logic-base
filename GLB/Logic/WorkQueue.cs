using GLB.Util.GameObject;
using GLB.Collections.Generic;

namespace GLB.Logic
{

    public interface IWork
    {
        WorkQueue.WorkResult Do(GameObjectContext goContext);
    }

    public class WorkQueue
    {

        public enum WorkResult { CONTINUE, PASS, END, ERROR }

        private class WorkEntity
        {
            public WorkEntity(IWork work, int count = 1)
            {
                Work = work;
                Count = count;
            }

            public IWork Work { get; }
            public int Count { get; set; }
        }

        private readonly PrioritySequence<WorkEntity, int> _workQueue = new();
        private readonly List<(WorkEntity Entity, int Priority)> _enqueueBuffer = new();

        public void Enqueue(IWork work, int count, int priority)
        {
            if (count <= 0) return;
            _enqueueBuffer.Add((new WorkEntity(work, count), priority));
        }

        public void Do(GameObjectContext goContext)
        {
            if (_enqueueBuffer.Count > 0)
            {
                foreach (var (entity, prio) in _enqueueBuffer)
                    _workQueue.Enqueue(entity, prio);
                _enqueueBuffer.Clear();
            }

            if (_workQueue.Count == 0)
                return;

            foreach (var element in _workQueue)
            {
                var workingEntity = element.Item1;
                var result = workingEntity.Work.Do(goContext);

                switch (result)
                {
                    case WorkResult.CONTINUE:
                        break;
                    case WorkResult.END:
                        break;
                    case WorkResult.ERROR:
                        break;
                    case WorkResult.PASS:
                        break;
                }
            }
        }

    }

}