using JsonBuilder.Core.Models.Messages;
using System.Windows;
using System.Windows.Controls;

namespace JsonBuilder.Views.TemplateSelectors
{
    public class MessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate PickConfirmTemplate { get; set; }
        public DataTemplate OrderInsertTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return item switch
            {
                PickConfirmMessage _ => PickConfirmTemplate,
                OrderInsertMessage _ => OrderInsertTemplate,
                _ => DefaultTemplate ?? base.SelectTemplate(item, container)
            };
        }
    }
}