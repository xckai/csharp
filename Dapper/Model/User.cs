using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DapperTest.Model
{
    class User
    {
        public string UUID { get; set; }

        public string NickName { set; get; }

        public string Name { set; get; }
    }
}
