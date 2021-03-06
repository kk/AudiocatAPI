﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Audiocat.Models;

namespace Audiocat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly AudiocatContext _context;

        public AudioController(AudiocatContext context, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _context = context;
        }

        // GET: api/Audio
        [HttpGet]
        public IEnumerable<AudioItem> GetAudioItem()
        {
            return _context.AudioItem;
        }

        // GET: api/Audio/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAudioItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var audioItem = await _context.AudioItem.FindAsync(id);

            if (audioItem == null)
            {
                return NotFound();
            }

            return Ok(audioItem);
        }

        // PUT: api/Audio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAudioItem([FromRoute] int id, [FromBody] AudioItem audioItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != audioItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(audioItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AudioItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Audio
        [HttpPost]
        public async Task<IActionResult> PostAudioItem([FromBody] AudioItem audioItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AudioItem.Add(audioItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAudioItem", new { id = audioItem.Id }, audioItem);
        }

        // DELETE: api/Audio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAudioItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var audioItem = await _context.AudioItem.FindAsync(id);
            if (audioItem == null)
            {
                return NotFound();
            }

            _context.AudioItem.Remove(audioItem);
            await _context.SaveChangesAsync();

            return Ok(audioItem);
        }

        // GET: api/Audio/Tags

        [HttpGet]
        [Route("searchQuery")]
        public async Task<List<AudioItem>> GetTagsItem([FromQuery] string searchQuery)
        {
            var audio = from a in _context.AudioItem
                        select a; //get all audio


            if (!String.IsNullOrEmpty(searchQuery)) //make sure user gave a tag to search
            {
                // gets audio objects with exact matches for tags or contains the query in the title
                audio = audio.Where(s => ( (s.Tag.ToLower().Equals(searchQuery.ToLower())) || (s.Title.ToLower().Contains(searchQuery.ToLower())) ));
            }

            var returned = await audio.ToListAsync(); //return the audio objects

            return returned;
        }

        private bool AudioItemExists(int id)
        {
            return _context.AudioItem.Any(e => e.Id == id);
        }
    }
}