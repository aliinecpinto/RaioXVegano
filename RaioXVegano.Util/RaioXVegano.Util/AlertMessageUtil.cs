using System.Collections.Generic;
using Xamarin.Forms;

namespace RaioXVegano.Util
{
    public static class AlertMessageUtil
    {
        public static void PrimaryMessage(Frame frameAlert, Label labelAlert, string message)
        {
            Message(frameAlert, labelAlert, message, "alert-primary", "label-primary");
        }

        public static void SecondaryMessage(Frame frameAlert, Label labelAlert, string message)
        {
            Message(frameAlert, labelAlert, message, "alert-secondary", "label-secondary");
        }

        public static void SuccessMessage(Frame frameAlert, Label labelAlert, string message)
        {
            Message(frameAlert, labelAlert, message, "alert-success", "label-success");
        }

        public static void DangerMessage(Frame frameAlert, Label labelAlert, Button emailAlert, string message)
        {
            Message(frameAlert, labelAlert, message, "alert-danger", "label-danger");
            emailAlert.IsVisible = true;
        }

        public static void WarningMessage(Frame frameAlert, Label labelAlert, string message)
        {
            Message(frameAlert, labelAlert, message, "alert-warning", "label-warning");
        }

        public static void InfoMessage(Frame frameAlert, Label labelAlert, string message)
        {
            Message(frameAlert, labelAlert, message, "alert-info", "label-info");
        }

        public static void LightMessage(Frame frameAlert, Label labelAlert, string message)
        {
            Message(frameAlert, labelAlert, message, "alert-light", "label-light");
        }

        public static void DarkMessage(Frame frameAlert, Label labelAlert, string message)
        {
            Message(frameAlert, labelAlert, message, "alert-dark", "label-dark");
        }

        private static void Message(Frame frameAlert, Label labelAlert, string message, string frameAlertClass, string labelAlertClass)
        {
            frameAlert.StyleClass = new List<string>() { "alert", frameAlertClass };
            frameAlert.IsVisible = true;

            labelAlert.StyleClass = new List<string>() { labelAlertClass };
            labelAlert.Text = message;
            labelAlert.IsVisible = true;
        }
    }
}
