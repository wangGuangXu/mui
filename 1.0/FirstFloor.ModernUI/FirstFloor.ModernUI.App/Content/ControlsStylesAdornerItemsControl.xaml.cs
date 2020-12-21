using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls.AttachPropertys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FirstFloor.ModernUI.App.Content
{
    /// <summary>
    /// Interaction logic for ControlsStylesAdornerItemsControl.xaml
    /// </summary>
    public partial class ControlsStylesAdornerItemsControl : UserControl
    {
        public ControlsStylesAdornerItemsControl()
        {
            InitializeComponent();
            this.DataContext = new AdornerItemViewModel();
        }
    }

    public class AdornerItemViewModel: NotifyPropertyChanged
    {
        private ObservableCollection<UserEntity> _users;
        /// <summary>
        /// 用户列表
        /// </summary>
        public ObservableCollection<UserEntity> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(() => this.Users);
            }
        }

        public AdornerItemViewModel()
        {
            Users = new ObservableCollection<UserEntity>();
            for (int i = 0; i < 25; i++)
            {
                Users.Add(new UserEntity() { Account=i.ToString("1000"),RealName="王"+i.ToString(),DepartmentName="研发部", OrganizeName="贝尔实验室" });
            }
        }
    }

    public class UserEntity
    {
        public UserEntity()
        {
            Id = Guid.NewGuid();
        }

        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 登录账户名
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        #endregion

        /// <summary>
        /// 性别
        /// </summary>
        public string GenderName { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrganizeName { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
    }
}
