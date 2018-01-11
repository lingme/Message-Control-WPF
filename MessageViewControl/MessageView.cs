using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MessageViewControl
{
    [TemplatePart(Name = Parid_carListBox)]
    [TemplatePart(Name = Parid_notiesListBox)]

    public class MessageView : Control
    {
        private const string Parid_carListBox = "Z_Parid_carListBox";
        private const string Parid_notiesListBox = "Z_Parid_notiesListBox";
        private const string Parid_upIcon = "Z_Parid_upIcon";
        private const string Parid_downIcon = "Z_Parid_downIcon";
        private const string Parid_tabItem_Noties = "Z_Parid_tabItem_Noties";
        private const string Parid_Noties_Icon = "Z_Parid_Noties_Icon";

        /// <summary>
        /// 集卡消息容器
        /// </summary>
        private ListBox _carListBox;
        /// <summary>
        /// 警告消息容器
        /// </summary>
        private ListBox _notiesListBox;
        /// <summary>
        /// （集卡）消息选中项
        /// </summary>
        private ListBoxItem _selectedListBoxItem_Car;
        /// <summary>
        /// （集卡）消息展开按钮          
        /// </summary>
        private Border _downPathButton_Car;
        /// <summary>
        /// （集卡）消息收起按钮
        /// </summary>
        private Border _upPathButton_Car;
        /// <summary>
        /// （警告）消息选中项
        /// </summary>
        private ListBoxItem _selectedListBoxItem_Noties;
        /// <summary>
        /// （警告）消息展开按钮     
        /// </summary>
        private Border _downPathButton_Noties;
        /// <summary>
        /// （警告）消息收起按钮
        /// </summary>
        private Border _upPathButton_Noties;
        /// <summary>
        /// 警告Tabltem
        /// </summary>
        private TabItem _tabItem_Noties;
        /// <summary>
        /// 警告！号图标
        /// </summary>
        private Border _Noties_Icon;

        private static MessageView _master_Mes_View;

        public static readonly DependencyProperty NotiesMessageListProperty = DependencyProperty.Register(
            "NotiesMessageListSourece",
            typeof(ObservableCollection<MessageItem>),
            typeof(MessageView),
            new PropertyMetadata(null,OnNotiesSourcesChanged,CoerceNotiesSrouces));

        public static readonly DependencyProperty CarMessageListProperty = DependencyProperty.Register(
            "CarMessageListSourece",
            typeof(ObservableCollection<MessageItem>),
            typeof(MessageView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedMsgProperty = DependencyProperty.Register(
            "SelectedMsg",
            typeof(MessageItem),
            typeof(MessageView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedNotiesProperty = DependencyProperty.Register(
            "SelectedNoties",
            typeof(MessageItem),
            typeof(MessageView),
            new PropertyMetadata(null));

        /// <summary>
        /// 警告消息
        /// </summary>
        public ObservableCollection<MessageItem> NotiesMessageListSourece
        {
            get { return (ObservableCollection<MessageItem>)GetValue(NotiesMessageListProperty); }
            set { SetValue(NotiesMessageListProperty, value); }
        }

        /// <summary>
        /// 集卡消息
        /// </summary>
        public ObservableCollection<MessageItem> CarMessageListSourece
        {
            get { return (ObservableCollection<MessageItem>)GetValue(CarMessageListProperty); }
            set { SetValue(CarMessageListProperty, value); }
        }

        /// <summary>
        /// 集卡消息选中项
        /// </summary>
        public MessageItem SelectedMsg
        {
            get { return (MessageItem)GetValue(SelectedMsgProperty); }
            set { SetValue(SelectedMsgProperty, value); }
        }

        /// <summary>
        /// 警告消息选中项
        /// </summary>
        public MessageItem SelectedNoties
        {
            get { return (MessageItem)GetValue(SelectedNotiesProperty); }
            set { SetValue(SelectedNotiesProperty, value); }
        }

        /// <summary>
        /// 警告信息集合更改事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnNotiesSourcesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MessageView MsgControl = (MessageView)d;
            _master_Mes_View = MsgControl;
            if (MsgControl.NotiesMessageListSourece != null && MsgControl.NotiesMessageListSourece.Count > 0)
            {
                MsgControl.NotiesMessageListSourece.CollectionChanged += NotiesMessageListSourece_CollectionChanged;
            }
        }

        private static void NotiesMessageListSourece_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (_master_Mes_View._Noties_Icon != null)
                {
                    _master_Mes_View._Noties_Icon.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        ///  强制转换
        /// </summary>
        /// <param name="d"></param>
        /// <param name="basevalue"></param>
        /// <returns></returns>
        private static object CoerceNotiesSrouces(DependencyObject d, object basevalue)
        {
            return basevalue;
        }

        /// <summary>
        /// 获取模板项
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((_carListBox = GetTemplateChild(Parid_carListBox) as ListBox) != null)
            {
                //设置集卡消息绑定
                Binding sourceBinding = new Binding("CarMessageListSourece") { Source = this };
                _carListBox.SetBinding(ListBox.ItemsSourceProperty, sourceBinding);

                //设置集卡选中消息绑定
                Binding carMsg = new Binding("SelectedMsg") { Source = this, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                _carListBox.SetBinding(ListBox.SelectedItemProperty, carMsg);

                //添加集卡选中事件
                _carListBox.SelectionChanged += CarListBox_SelectionChanged;
            }
            if ((_notiesListBox = GetTemplateChild(Parid_notiesListBox) as ListBox) != null)
            {
                //设置警告消息绑定
                Binding sourceBinding = new Binding("NotiesMessageListSourece") { Source = this };
                _notiesListBox.SetBinding(ListBox.ItemsSourceProperty, sourceBinding);

                //设置警告消息绑定
                Binding carMsg = new Binding("SelectedNoties") { Source = this, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                _notiesListBox.SetBinding(ListBox.SelectedItemProperty, carMsg);

                //添加警告选中事件
                _notiesListBox.SelectionChanged += NotiesListBox_SelectionChanged;
            }
        }

        /// <summary>
        /// 集卡选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedListBoxItem_Car = _carListBox.ItemContainerGenerator.ContainerFromItem(SelectedMsg) as ListBoxItem;
            if (_selectedListBoxItem_Car != null)
            {
                _selectedListBoxItem_Car.Height = double.NaN;
                if ((_upPathButton_Car = _selectedListBoxItem_Car.Template.FindName(Parid_upIcon, _selectedListBoxItem_Car) as Border) != null)
                {
                    _upPathButton_Car.MouseLeftButtonDown += Car_MouseLeftButtonDown;
                    _upPathButton_Car.Visibility = Visibility.Visible;
                }
                if ((_downPathButton_Car = _selectedListBoxItem_Car.Template.FindName(Parid_downIcon, _selectedListBoxItem_Car) as Border) != null)
                {
                    _downPathButton_Car.MouseLeftButtonDown += Car_MouseLeftButtonDown;
                    _downPathButton_Car.Visibility = Visibility.Collapsed;
                }

                foreach (var item in CarMessageListSourece.Where(x => x != SelectedMsg).Select(x => x))
                {
                    ListBoxItem noSelectedItem = _carListBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                    Border noSelectedItem_up = _selectedListBoxItem_Car.Template.FindName(Parid_upIcon, noSelectedItem) as Border;
                    Border noSelectedItem_down = _selectedListBoxItem_Car.Template.FindName(Parid_downIcon, noSelectedItem) as Border;
                    if (noSelectedItem != null && noSelectedItem_down != null && noSelectedItem_up != null)
                    {
                        noSelectedItem_up.Visibility = Visibility.Collapsed;
                        noSelectedItem_down.Visibility = Visibility.Visible;
                        noSelectedItem.Height = 30;
                    }
                }
            }
        }

        /// <summary>
        /// 警告选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotiesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedListBoxItem_Noties = _notiesListBox.ItemContainerGenerator.ContainerFromItem(SelectedNoties) as ListBoxItem;
            if (_selectedListBoxItem_Noties != null)
            {
                _selectedListBoxItem_Noties.Height = double.NaN;
                if ((_upPathButton_Noties = _selectedListBoxItem_Noties.Template.FindName(Parid_upIcon, _selectedListBoxItem_Noties) as Border) != null)
                {
                    _upPathButton_Noties.MouseLeftButtonDown += Noties_MouseLeftButtonDown;
                    _upPathButton_Noties.Visibility = Visibility.Visible;
                }
                if ((_downPathButton_Noties = _selectedListBoxItem_Noties.Template.FindName(Parid_downIcon, _selectedListBoxItem_Noties) as Border) != null)
                {
                    _downPathButton_Noties.MouseLeftButtonDown += Noties_MouseLeftButtonDown;
                    _downPathButton_Noties.Visibility = Visibility.Collapsed;
                }

                foreach (var item in NotiesMessageListSourece.Where(x => x != SelectedNoties).Select(x => x))
                {
                    ListBoxItem noSelectedItem = _notiesListBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                    Border noSelectedItem_up = _selectedListBoxItem_Noties.Template.FindName(Parid_upIcon, noSelectedItem) as Border;
                    Border noSelectedItem_down = _selectedListBoxItem_Noties.Template.FindName(Parid_downIcon, noSelectedItem) as Border;
                    if (noSelectedItem != null && noSelectedItem_down != null && noSelectedItem_up != null)
                    {
                        noSelectedItem_up.Visibility = Visibility.Collapsed;
                        noSelectedItem_down.Visibility = Visibility.Visible;
                        noSelectedItem.Height = 30;
                    }
                }
            }
            if ((_tabItem_Noties = GetTemplateChild(Parid_tabItem_Noties) as TabItem) != null)
            {
                if((_Noties_Icon = _tabItem_Noties.Template.FindName(Parid_Noties_Icon, _tabItem_Noties) as Border) != null)
                {
                    SelectedNoties.IsRead = true;
                    if (NotiesMessageListSourece.Where(x=>x.IsRead == false).ToList().Count == 0)
                    {
                        _Noties_Icon.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        _Noties_Icon.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        /// <summary>
        /// 集卡消息列表收起展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Car_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border pat = sender as Border;
            if(_upPathButton_Car != null && _downPathButton_Car != null && _selectedListBoxItem_Car !=null )
            {
                _upPathButton_Car.Visibility = pat.Name == Parid_downIcon ? Visibility.Visible : Visibility.Collapsed;
                _downPathButton_Car.Visibility = pat.Name == Parid_downIcon ? Visibility.Collapsed : Visibility.Visible;
                _selectedListBoxItem_Car.Height = pat.Name == Parid_downIcon ? double.NaN : 30f;
            }
        }

        /// <summary>
        /// 集卡消息列表收起展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Noties_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border pat = sender as Border;
            if (_upPathButton_Noties != null && _downPathButton_Noties != null && _selectedListBoxItem_Noties != null)
            {
                _upPathButton_Noties.Visibility = pat.Name == Parid_downIcon ? Visibility.Visible : Visibility.Collapsed;
                _downPathButton_Noties.Visibility = pat.Name == Parid_downIcon ? Visibility.Collapsed : Visibility.Visible;
                _selectedListBoxItem_Noties.Height = pat.Name == Parid_downIcon ? double.NaN : 30f;
            }
        }

        /// <summary>
        /// 覆盖默认样式
        /// </summary>
        static MessageView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageView), new FrameworkPropertyMetadata(typeof(MessageView)));
        }
    }
}
