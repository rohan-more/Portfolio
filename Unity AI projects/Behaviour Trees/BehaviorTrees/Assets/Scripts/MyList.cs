using System.Collections.Generic;
using System.Text;

namespace AISandbox
{
    public class MyList<T> : List<T>
    {
        StringBuilder builder = new StringBuilder();
        public new void Add(T item)
        {
            //string temp = (Count).ToString() + " " + item as string;
            //CanvasManager.currentFSMState.text = item as string;
            CanvasManager.notification.text = builder.Append(item).Append("\n").ToString();
            base.Add(item);
            CanvasManager.currentMessageNumber = Count - 1;
        }
    }
}
