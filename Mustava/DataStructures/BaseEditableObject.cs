using System.ComponentModel;
using Mustava.Helper;
using Mustava.Helpers;

namespace Mustava.DataStructures
{

    //todo : çalışmıyor.
    public class BaseEditableObject<T> : IEditableObject where T : class
    {
        private readonly T _this;
        private T backupCopy;
        private bool inEdit;

        public BaseEditableObject(T __this)
        {
            _this = __this;
        }


        public void BeginEdit()
        {
            if (inEdit)
                return;

            inEdit = true;
            backupCopy = MemberwiseClone() as T;
        }

        public void EndEdit()
        {
            if (!inEdit)
                return;

            inEdit = false;
        }

        public void CancelEdit()
        {
            if (!inEdit)
                return;

            inEdit = false;

            _this.SetAllMyMembersForIEditable(backupCopy);
            backupCopy = default(T);
        }

    }
}