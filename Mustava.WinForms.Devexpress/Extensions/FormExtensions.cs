using System;
using System.Linq.Expressions;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace Mustava.WinForms.Devexpress.Extensions
{
    public static class FormExtensions
    {
        public static void DoBatchWork(this Form form, Action expression, string title, string workMessage = "İşlem Devam Ediyor...")
        {
            SplashScreenManager.ShowDefaultWaitForm(form, true, true, title, workMessage);

            expression.Invoke();

            SplashScreenManager.CloseDefaultWaitForm();
        }

        public static void DoWork(this Form form, Expression<Action> expression, string title, string workMessage = "İşlem Devam Ediyor...")
        {
            SplashScreenManager.ShowDefaultWaitForm(form, true, true, title, workMessage);

            expression.Compile().Invoke();

            SplashScreenManager.CloseDefaultWaitForm();
        }

        public static T DoWork<T>(this Form form, Expression<Func<T>> expression, string title, string workMessage = "İşlem Devam Ediyor...")
        {
            SplashScreenManager.ShowDefaultWaitForm(form, true, true, title, workMessage);

            var result = expression.Compile().Invoke();

            SplashScreenManager.CloseDefaultWaitForm();

            return result;
        }
    }
}