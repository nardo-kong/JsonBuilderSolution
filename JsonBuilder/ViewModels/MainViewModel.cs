using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JsonBuilder.Core.Models;
using JsonBuilder.Core.Models.Messages;
using JsonBuilder.Core.Utilities;
using System.Collections.ObjectModel;

namespace JsonBuilder.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // 当前选中的消息类型
        [ObservableProperty]
        private MessageBase? _selectedMessageType;

        // 所有可用消息类型集合
        public ObservableCollection<MessageBase> MessageTypes { get; } = new();

        // 当前正在编辑的消息对象
        [ObservableProperty]
        private MessageBase? _currentMessage;

        public MainViewModel()
        {
            InitializeMessageTypes();
        }

        private void InitializeMessageTypes()
        {
            // 初始化预定义消息类型
            MessageTypes.Add(new PickConfirmMessage());
            MessageTypes.Add(new OrderInsertMessage());
        }

        [RelayCommand]
        private void AddNestedItem()
        {
            if (CurrentMessage != null)
            {
                var nested = new PickConfirmMessage(); // 可替换为动态选择
                CurrentMessage.NestedMessages.Add(nested);
            }
        }

        [RelayCommand]
        private void GenerateJson()
        {
            if (CurrentMessage == null) return;

            var json = JsonGenerator.Generate(CurrentMessage);
            // 这里可以添加输出到UI或文件的逻辑
        }

        // 当选择消息类型时自动创建新实例
        partial void OnSelectedMessageTypeChanged(MessageBase? value)
        {
            CurrentMessage = value?.CreateNewInstance();
        }
    }
}