using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JsonBuilder.Core.Models;
using JsonBuilder.Core.Models.Messages;
using JsonBuilder.Core.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace JsonBuilder.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // 当前选中的消息类型
        [ObservableProperty]
        private MessageBase? _selectedMessageType;

        // 当前正在编辑的消息对象
        [ObservableProperty]
        private MessageBase? _currentMessage;

        // 添加新属性
        [ObservableProperty]
        private MessageBase? _selectedNestedMessage;

        // 所有可用消息类型集合
        public ObservableCollection<MessageBase> MessageTypes { get; } = new();

        // JSON 输出内容
        [ObservableProperty]
        private string _outputJson = string.Empty;

        public MainViewModel()
        {
            InitializeMessageTypes();
        }

        private void InitializeMessageTypes()
        {
            // 初始化预定义消息类型
            MessageTypes.Add(new PickConfirmMessage());
            MessageTypes.Add(new OrderInsertMessage());
            Debug.WriteLine($"Initialized {MessageTypes.Count} message types");
        }

        [RelayCommand]
        private void AddNestedItem()
        {
            if (CurrentMessage != null)
            {
                var nested = new LineResponseMessage(); // 添加 LineResponse 嵌套项
                CurrentMessage.NestedMessages.Add(nested);
                SelectedNestedMessage = nested;
                if (CurrentMessage.NestedMessages.Any(message => message == null))
                {
                    Debug.WriteLine("NestedMessages contains null items");
                } else
                {
                    Debug.WriteLine("NestedMessages not null items");
                }
            }
        }

        [RelayCommand]
        private void GenerateJson()
        {
            if (CurrentMessage == null)
            {
                OutputJson = "Error: No message selected";
                return;
            }

            // 校验所有参数非空
            Dictionary<string, object> validatedParameters;
            IList<string> errorMessages;
            bool isValid = ParameterValidation.ValidateAllParameters(CurrentMessage, out validatedParameters, out errorMessages);
            if (!isValid)
            {
                OutputJson = $"Validation Error:\n{string.Join("\n", errorMessages)}";
                return;
            }

            try
            {
                OutputJson = JsonGenerator.Generate(CurrentMessage);
            }
            catch (Exception ex)
            {
                OutputJson = $"Generation Error:\n{ex.Message}";
            }
        }
        [RelayCommand]
        private void RemoveNested()
        {
            if (CurrentMessage != null && SelectedNestedMessage != null)
            {
                CurrentMessage.NestedMessages.Remove(SelectedNestedMessage);
                SelectedNestedMessage = null;
            }
        }
        [RelayCommand]
        private void Copy()
        {
            if (OutputJson != null)
            {
                // 复制 OutputJson 的值到剪贴板
                Clipboard.SetDataObject(OutputJson);
            }
        }

        // 当选择消息类型时自动创建新实例
        partial void OnSelectedMessageTypeChanged(MessageBase? oldValue, MessageBase? newValue)
        {
            CurrentMessage = newValue?.CreateNewInstance();
            OutputJson = string.Empty;
        }



    }
}