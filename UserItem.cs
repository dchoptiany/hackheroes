using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    public partial class UserItem : UserControl
    {
        public UserItem()
        {
            InitializeComponent();
        }

        #region Properties

        private string _userName;
        private Image _avatar;

        [Category("Custom Props")]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; labelUserName.Text = _userName; }
        }

        [Category("Custom Props")]
        public Image  Avatar
        {
            get { return _avatar; }
            set { _avatar = value; pictureBoxAvatar.Image = _avatar; }
        }

        #endregion
    }
}
