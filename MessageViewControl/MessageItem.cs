using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MessageViewControl
{
    public class MessageItem : INotifyPropertyChanged
    {
        private string _typeName;
        /// <summary>
        /// 消息标题类型（左部分类型（例：卡车编号））
        /// </summary>
        public string TypeName
        {
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
        public string TypeNum
        {
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
        public bool IsRead
        {
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
        public string DetailedInfo
        {
            get
            {
                string str = string.Empty;
                if(ItemList!=null && ItemList.Count >0)
                {
                    foreach (var item in ItemList)
                    {
                        str += item.Key.PadRight(10, ' ') + "\t\t：  " + item.Value + "\n\n";
                    }
                }
                return str.TrimEnd(new char[] {'\n','\n'});
            }
        }

        private Dictionary<string,string> _itemList;
        /// <summary>
        /// 消息详情（由于消息详情可能是多条）
        /// </summary>
        public Dictionary<string,string> ItemList
        {
            get { return _itemList; }
            set
            {
                _itemList = value;
                OnPropertyChanged("ItemList");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
