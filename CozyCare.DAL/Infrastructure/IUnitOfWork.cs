using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.DAL.Infrastructure
{
	public interface IUnitOfWork : IDisposable
	{
		public void Commit();
		public Task CommitAsync();
	}
}
