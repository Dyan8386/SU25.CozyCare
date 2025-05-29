using CozyCare.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.DAL.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly CozyCareContext _context;

		public UnitOfWork(CozyCareContext context)
		{
			_context = context;

		}

		public void Commit()
		{
			this._context.SaveChanges();
		}

		public async Task CommitAsync()
		{
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"🔥 Error: {ex.InnerException?.Message ?? ex.Message}");
				throw;
			}
		}

		public void Dispose()
		{
			this._context.Dispose();
		}
	}
}
