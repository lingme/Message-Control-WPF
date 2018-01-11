# MsgTab Control WPF
---
WPF实现的类型消息中心的控件
####View代码：
```C
<message:MessageView  Grid.Row="0" Grid.RowSpan="1" CarMessageListSourece="{Binding CarMessageList}" NotiesMessageListSourece="{Binding NotiesMessageList}"></message:MessageView>
```

#### 依赖属性说明：
>**CarMessageListSourece** 
集卡消息源

>**NotiesMessageListSourece**
警告消息源

这两个依赖属性都是MessageItem类型，具体属性如下：

```C {.line-numbers}
public class MessageItem: INotifyPropertyChanged {
    private string _typeName;
    /// <summary>
    /// 消息标题类型（左部分类型（例：卡车编号））
    /// </summary>
    public string TypeName {
        get { return _typeName; }
        set 
        {
            _typeName = value;
            OnPropertyChanged("TypeName");
        }
    }

    private string _typeNum;
    /// <summary>
    /// 消息标题编号（右部分类型（例：04848））
    /// </summary>
    public string TypeNum {
        get { return _typeNum; }
        set 
        {
            _typeNum = value;
            OnPropertyChanged("TypeNum");
        }
    }

    private bool _isRead = false;
    /// <summary>
    /// 消息已读
    /// </summary>
    public bool IsRead {
        get { return _isRead; }
        set 
        {
            _isRead = value;
            OnPropertyChanged("IsRead");
        }
    }

    /// <summary>
    /// 消息详情（拼接）
    /// </summary>
    public string DetailedInfo {
        get {
            string str = string.Empty;
            if (ItemList != null && ItemList.Count > 0) {
                foreach(var item in ItemList) {
                    str += item.Key.PadRight(10, ' ') + "\t\t：  " + item.Value + "\n\n";
                }
            }
            return str.TrimEnd(new char[] { '\n', '\n' });
        }
    }

    private Dictionary < string, string > _itemList;
    /// <summary>
    /// 消息详情（由于消息详情可能是多条）
    /// </summary>
    public Dictionary < string, string > ItemList {
        get { return _itemList; }
        set 
        {
            _itemList = value;
            OnPropertyChanged("ItemList");
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName) {
        PropertyChangedEventHandler handler = this.PropertyChanged;
        if (handler != null) {
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
```

![A](https://github.com/lingme/Picture_Bucket/raw/master/MsgTab_Control_WPF_img/index_1.jpg)

![A](https://github.com/lingme/Picture_Bucket/raw/master/MsgTab_Control_WPF_img/index_2.jpg)

![A](https://github.com/lingme/Picture_Bucket/raw/master/MsgTab_Control_WPF_img/index_3.jpg)

![A](https://github.com/lingme/Picture_Bucket/raw/master/MsgTab_Control_WPF_img/index_4.jpg)


