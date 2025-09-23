using GLB.Util;
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

        private readonly PrioritySequence<WorkEntity, int> _workSequence = new();
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
                    _workSequence.Enqueue(entity, prio);
                _enqueueBuffer.Clear();
            }

            if (_workSequence.Count == 0)
                return;

            for (int i = 0; i < _workSequence.Count; ++i)
            {
                var workingEntity = _workSequence[i];
                var result = workingEntity.Work.Do(goContext);

                switch (result)
                {
                    case WorkResult.CONTINUE:
                        return;
                    case WorkResult.END:
                        _workSequence.RemoveAt(i);
                        return;
                    case WorkResult.ERROR:
                        throw new Exception("work returns error : " + workingEntity);
                    case WorkResult.PASS:
                        break;
                }
            }
        }

    }

}