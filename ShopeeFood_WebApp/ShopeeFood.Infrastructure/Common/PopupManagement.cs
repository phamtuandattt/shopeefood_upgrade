using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.Infrastructure.Common
{
    public static class PopupManagement
    {
        public static string DELETE_FAIL_MESSAGE = "Unable to delete the order. Please try again later.";
        public static string DELETE_SUCCESS_MESSAGE = "The item has been successfully deleted from your account.";

        public static string UPDATE_CUSTOMER_ADDRESS_SUCCESS_MESSAGE = "Your request has been processed successfully!";
        public static string UPDATE_CUSTOMER_ADDRESS_FAIL_MESSAGE = "Unable to update the address. Please try again later.";

        public static string ADD_CUSTOMER_ADDRESS_SUCCESS_MESSAGE = "Your request has been processed successfully!";
        public static string ADD_CUSTOMER_ADDRESS_FAIL_MESSAGE = "Unable to add the address. Please try again later.";

        static IDictionary<PopupType, string> PopupTypes;
        static IDictionary<PopupAction, string> PopupActions;

        static PopupManagement()
        {
            PopupTypes = new Dictionary<PopupType, string>
            {
                {PopupType.Success, "success"},
                {PopupType.Error, "error"},
                {PopupType.Warning, "warning"},
                {PopupType.Info, "info"},
                {PopupType.Loading, "loading"}
            };

            PopupActions = new Dictionary<PopupAction, string>
            {
                {PopupAction.Add, "Add"},
                {PopupAction.Update, "Update"},
                {PopupAction.Delete, "Delete"}
            };
        }
        public static string GetPopupType(PopupType key)
        {
            return PopupTypes[key];
        }

        public static string GetTitlePopup(PopupAction action, bool success)
        {
            return success ? PopupActions[action] + "success": PopupActions[action] + "Failed";
        }
    }

    public enum PopupType
    {
        Success = 1,
        Error = 2,
        Warning = 3,
        Info = 4,
        Loading = 5,
    }

    public enum PopupAction
    {
        Add,
        Delete,
        Update
    }
}