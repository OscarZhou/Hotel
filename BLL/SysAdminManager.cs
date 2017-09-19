using DAL;
using Models;

namespace BLL
{
    public class SysAdminManager
    {
        private SysAdminService objSysAdminService = new SysAdminService();

        public SysAdmin Login(SysAdmin objAdmin)
        {
            return objSysAdminService.Login(objAdmin);
        }
    }
}
