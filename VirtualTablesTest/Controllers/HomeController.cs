using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VirtualTablesTest.Models;
using VirtualTablesTest.Services;

namespace VirtualTablesTest.Controllers
{
    public class HomeController : Controller
    {
        private Repository<VirtualTable> _tableRepo;
        private IDbContext _context;
        //private WorkTestDbContext _context;


        public HomeController(IContextFactory factory, Repository<VirtualTable> tableRepo)
        {
            _tableRepo = tableRepo;
            _context = factory.DbContext;

            
            
        }


        public IActionResult Index()
        {

            var tables = _context.Set<VirtualTable>().Include(r => r.Rows)
                                                     .ThenInclude(v => v.Values)
                                                     .Include(c => c.Columns)
                                                     .ToList();



            return View(tables);
            //return Ok(_context.Set<VirtualTable>().Include(p => p.Rows)); //uncomment for transient demo
            //return Ok(_tableRepo.GetAll().Include(p => p.Rows));          // uncomment for scoped instance demo
        }

        [HttpPost]
        public IActionResult AddTable([FromForm] VirtualTable formData)
        {

            _context.Set<VirtualTable>().Add(formData);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Column(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var table = _context.Set<VirtualTable>().Include(r => r.Rows)
                                                    .ThenInclude(v => v.Values)
                                                    .Include(c => c.Columns)
                                                    .ToList()
                                                    .First(p => p.Id == id);

            var columns = table.Columns == null ? new List<VirtualColumn>() : table.Columns.ToList();


            return View("Column", new FooWrapper
            {
                Table = table,
                Columns = columns
            });
        }

        [HttpPost]
        public IActionResult AddColumn([FromQuery]long tableId, [FromForm] VirtualColumn column)
        {

            column.VirtualTableId = tableId;

            _context.Set<VirtualColumn>().Add(column);

            foreach (var row in _context.Set<VirtualRow>().Include(c => c.Values)
                                                          .Include(t => t.Table)
                                                          .Where(p => p.Table.Id == tableId))
            {
                row.Values.Add(new VirtualValue { Column = column, Value = column.DefaultValue });
            }

            _context.SaveChanges();

            return RedirectToAction("Column", new { id = tableId });
        }

        [HttpGet]
        public IActionResult DeleteColumn(long id)
        {
            var col = _context.Set<VirtualColumn>().Include(t => t.Table)
                                                   .FirstOrDefault(p => p.Id == id);
            var tableId = col.VirtualTableId;

            var values = _context.Set<VirtualValue>().Where(p => p.Column.Id == id);

            foreach (var val in values)
            {
                _context.Set<VirtualValue>().Remove(val);
            }

            _context.Set<VirtualColumn>().Remove(col);
            _context.SaveChanges();

            return RedirectToAction("Column", new { id = tableId });
        }


        [HttpGet]
        public IActionResult DeleteTable(long id)
        {
            var table = _context.Set<VirtualTable>().FirstOrDefault(p => p.Id == id);

            _context.Set<VirtualTable>().Remove(table);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Row(int id)
        {

            var modelTable = _context.Set<VirtualTable>().Include(table => table.Rows)
                                                             .ThenInclude(row => row.Values)
                                                         .Include(c => c.Columns)
                                                         .FirstOrDefault(p => p.Id == id);

            var newRow = new VirtualRow { Table = modelTable };

            newRow.Values = modelTable.Columns.Select(c => new VirtualValue { Column = c, Row = newRow }).ToList();

            return View("Row", newRow);
        }

        [HttpPost]
        public IActionResult AddRow([FromQuery]long tableId, [FromForm] VirtualRow row)
        {
            var table = _context.Set<VirtualTable>().Include(r => r.Rows)
                                                    .Include(c => c.Columns)
                                                    .FirstOrDefault(p => p.Id == tableId);

            table.Rows.Add(row);
            _context.SaveChanges();

            var afterUpdate = _context.Set<VirtualTable>().Include(p => p.Rows).FirstOrDefault(p => p.Id == tableId);

            return RedirectToAction("Row", new { id = tableId });
        }

        [HttpGet]
        public IActionResult DeleteRow(long id)
        {
            var row = _context.Set<VirtualRow>().FirstOrDefault(r => r.Id == id);
            

            var tableId = row.VirtualTableId;

            _context.Set<VirtualRow>().Remove(row);

            _context.SaveChanges();

            return RedirectToAction("Row", new { id = tableId });
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
