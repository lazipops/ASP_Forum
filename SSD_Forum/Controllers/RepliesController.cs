using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSD_Forum.Data;
using SSD_Forum.Models;


namespace SSD_Forum.Controllers
{
    public class RepliesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RepliesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Replies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Replies.ToListAsync());
        }

        // GET: Replies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies
                .FirstOrDefaultAsync(m => m.ReplyId == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

        // GET: Replies/Create
        [Authorize]
        public IActionResult Create(int? id)
        {
            return View();
        }

        // POST: Replies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(int id, [Bind("ReplyId,Content,OriginPost,UserWhoReplied,DateReplied")] Reply reply)
        {
            if (ModelState.IsValid)
            {
                reply.DateReplied = DateTime.Now;
                reply.UserWhoReplied = User.Identity.Name;
                reply.OriginPost = id;
                _context.Add(reply);
                await _context.SaveChangesAsync();
                return Redirect("../../Threads/Details/" + id);
            }
            return Redirect("../../Threads/Details/" + id);
        }

        // GET: Replies/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies.FindAsync(id);
            if (reply == null)
            {
                return NotFound();
            } else if(reply.UserWhoReplied != User.Identity.Name)
            {
                return Redirect("../../Threads/Details/" + reply.OriginPost); 
            }
            return View(reply);
        }

        // POST: Replies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ReplyId,Content,OriginPost,UserWhoReplied,DateReplied")] Reply reply)
        {
            if (id != reply.ReplyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    reply.DateReplied = DateTime.Now;
                    reply.UserWhoReplied = User.Identity.Name;
                    reply.OriginPost = reply.OriginPost;
                    _context.Update(reply);
                    if (reply.UserWhoReplied != User.Identity.Name)
                    {
                        return Redirect("../../Threads/Details/" + reply.OriginPost);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplyExists(reply.ReplyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("../../Threads/Details/" + reply.OriginPost);
            }
            return Redirect("../../Threads/Details/" + reply.OriginPost);
        }

        // GET: Replies/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies
                .FirstOrDefaultAsync(m => m.ReplyId == id);
            if (reply == null)
            {
                return NotFound();
            } else if (reply.UserWhoReplied != User.Identity.Name) {
                return Redirect("../../Threads/Details/" + reply.OriginPost);
            }

            return View(reply);
        }

        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reply = await _context.Replies.FindAsync(id);
            if (reply.UserWhoReplied != User.Identity.Name)
            {
                return Redirect("../../Threads/Details/" + reply.OriginPost);
            }
            _context.Replies.Remove(reply);
            await _context.SaveChangesAsync();
            return Redirect("../../Threads/Details/" + reply.OriginPost);
        }

        private bool ReplyExists(int id)
        {
            return _context.Replies.Any(e => e.ReplyId == id);
        }
    }
}
