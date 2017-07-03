using System.Collections.Generic;

namespace AISandbox
{
    public class BehaviorTree
    {
        public enum Result
        {
            FAILURE,
            SUCCESS,
            RUNNING,
            UNKNOWN,
        }
        public static string ConvertToString(Result result)
        {
            switch (result)
            {
                case Result.FAILURE:
                    return "Failure";
                case Result.SUCCESS:
                    return "Success";
                case Result.RUNNING:
                    return "Running";
                default:
                    return null;
            }
        }
        private Node _rootNode;
        private Dictionary<string, object> _dataContext;
        public Dictionary<string, object> dataContext
        {
            get
            {
                return _dataContext;
            }
        }
        public BehaviorTree(Node rootNode)
        {
            _rootNode = rootNode;
            _dataContext = new Dictionary<string, object>();
        }

        public void AddToDictionary(string key, object value)
        {
            _dataContext[key] = value;
        }

        public Result Process()
        {
            return _rootNode.Process();
        }
    }
}

