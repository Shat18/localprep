using LocalPrep.Data.Repository;
using LocalPrep.Data.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPrep.Data.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AspNetUser> UserRepository { get; }

        IRepository<Vendor> VendorRepository { get; }
        void Save();
        int Commit();
    }
    public partial class UnitOfWork :IUnitOfWork
    {
        private IRepository<AspNetUser> _userRepository;
        private IRepository<Vendor> _VendorRepository;
       
        localprepdbEntities _context = new localprepdbEntities();
        public IRepository<AspNetUser> UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new Repository<AspNetUser>(_context);

                return _userRepository;
            }
        }


        public IRepository<Vendor> VendorRepository
        {
            get
            {
                if (_VendorRepository == null)
                    _VendorRepository = new Repository<Vendor>(_context);

                return _VendorRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public int Commit()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
