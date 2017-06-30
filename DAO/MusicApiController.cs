using Project2016.Data_Tier;
using Project2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project2016.DAO
{
    public class MusicApiController : ApiController
    {
        Project2016Entities objDB = new Project2016Entities();
        UserModel objUserMdl = new UserModel();
        //MusicViewModel objMusicVwMdl = new MusicViewModel();
        List<MusicViewModel> lstMusicVwMdl = new List<MusicViewModel>();

        public bool isUserExists(string Username, string Password)
        {
            var query = ((from user in objDB.users
                          where user.user_name == Username && user.password == Password && user.isactive == 1
                          select user)).FirstOrDefault();

            if (query != null)
            {
                return true;
            }

            return false;
        }

        public bool registerUser(string Username, string Password)
        {
            bool isSuccess;

            user objUser = new user();
            objUser.user_name = Username;
            objUser.password = Password;
            objUser.isactive = 1;

            try { 
                var query = objDB.users.Add(objUser);
                objDB.SaveChanges();
                isSuccess = true;
            }
            catch(Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public int getUserIdByUsername(string username)
        {
            var userid = ((from user in objDB.users
                           where user.user_name == username
                           select user.user_id)).FirstOrDefault();
            
            return Convert.ToInt16(userid);
        }

        public List<MusicViewModel> searchHistory(int userid)
        {
            var query = (from history in objDB.playeds
                         where history.user_id == userid
                         select history).ToList();

            if(query != null)
            {
                foreach(var list in query)
                {
                    var albumname = (from album in objDB.albums
                                     where album.album_id == list.album_id
                                     select album.album_name).FirstOrDefault();

                    var artistname = (from artist in objDB.artists
                                      where artist.artist_id == list.artist_id
                                      select artist.artist_name).FirstOrDefault();

                    var track = (from track1 in objDB.tracks
                                     where track1.track_id == list.track_id
                                     select new { track1.track_name, track1.time }).FirstOrDefault();

                    MusicViewModel objMusicVwMdl = new MusicViewModel();
                    objMusicVwMdl.album_id = list.album_id;
                    objMusicVwMdl.artist_id = list.artist_id;
                    objMusicVwMdl.track_id = list.track_id;
                    objMusicVwMdl.played = list.played1;
                    objMusicVwMdl.user_id = list.user_id;
                    objMusicVwMdl.album_name = albumname;
                    objMusicVwMdl.artist_name = artistname;
                    objMusicVwMdl.track_name = track.track_name;
                    objMusicVwMdl.track_time = track.time;

                    lstMusicVwMdl.Add(objMusicVwMdl);
                }
            }

            return lstMusicVwMdl;
        }

        public List<string> searchString(string searchString, string optionString)
        {
            List<string> lstResult = new List<string>();

            searchString = searchString.ToLower();

            var query = ((from tracks in objDB.tracks
                          join albums in objDB.albums
                          on tracks.album_id equals albums.album_id                          
                          join artists in objDB.artists
                          on tracks.artist_id equals artists.artist_id
                          select new { tracks.track_name, albums.album_name, artists.artist_name })).ToList();

            if (query != null) {

                //var res = query.Where(r => r.track_name.Contains(searchString)).ToList();

                //foreach (var r in query)
                //{
                //    string str = r.track_name.Trim().ToString() + "|" + r.album_name.Trim().ToString() + "|" + r.artist_name.Trim().ToString();
                //    lstResult.Add(str);
                //}                                

                if (optionString == "all")
                {
                    var res = query.Where(r => r.track_name.ToLower().Trim().Contains(searchString) || r.album_name.ToLower().Trim().Contains(searchString)
                    || r.artist_name.ToLower().Trim().Contains(searchString)).ToList();

                    foreach (var r in res)
                    {
                        string str = r.track_name.Trim().ToString() + "|" + r.album_name.Trim().ToString() + "|" + r.artist_name.Trim().ToString();
                        lstResult.Add(str);
                    }
                }
                else if(optionString == "track")
                {
                   var res = query.Where(r => r.track_name.ToLower().Trim().Contains(searchString)).ToList();

                    foreach (var r in res)
                    {
                        string str = r.track_name.Trim().ToString() + "|" + r.album_name.Trim().ToString() + "|" + r.artist_name.Trim().ToString();
                        lstResult.Add(str);
                    }
                }
                else if (optionString == "album")
                {
                    var res = query.Where(r => r.album_name.ToLower().Trim().Contains(searchString)).ToList();

                    foreach (var r in res)
                    {
                        string str = r.track_name.Trim().ToString() + "|" + r.album_name.Trim().ToString() + "|" + r.artist_name.Trim().ToString();
                        lstResult.Add(str);
                    }
                }
                else if (optionString == "artist")
                {
                    var res = query.Where(r => r.artist_name.ToLower().Trim().Contains(searchString)).ToList();

                    foreach (var r in res)
                    {
                        string str = r.track_name.Trim().ToString() + "|" + r.album_name.Trim().ToString() + "|" + r.artist_name.Trim().ToString();
                        lstResult.Add(str);
                    }
                }

            }

            return lstResult;
        }

        public bool saveHistory(int user_id, string track, string album, string artist)
        {
            bool isSuccessful;

            if (track.Trim().Contains("&amp;"))
                track = track.Trim().Replace("&amp;", "&");
            if (album.Trim().Contains("&amp;"))
                album = album.Trim().Replace("&amp;", "&");
                if (artist.Trim().Contains("&amp;"))
                artist = artist.Trim().Replace("&amp;", "&");

            try
            {
                var artistid = ((from artists in objDB.artists
                                 where artists.artist_name.Trim() == artist.Trim()
                                 select artists.artist_id)).FirstOrDefault();

                int artist_id = Int16.Parse(artistid.ToString());

                var albumid = ((from albums in objDB.albums
                                 where albums.album_name.Trim() == album.Trim()
                                 select albums.album_id)).FirstOrDefault();

                int album_id = Int16.Parse(albumid.ToString());

                var trackid = ((from tracks in objDB.tracks
                                where tracks.track_name.Trim() == track.Trim() && tracks.album_id == album_id && tracks.artist_id == artist_id
                                select tracks.track_id)).FirstOrDefault();

                int track_id = Int16.Parse(trackid.ToString());

                var query = ((from p in objDB.playeds
                              where p.user_id == user_id && p.track_id == track_id && p.album_id == album_id && p.artist_id == artist_id
                              select p)).FirstOrDefault();
                
                if (query == null)
                {
                    played objPlayed = new played();
                    objPlayed.album_id = album_id;
                    objPlayed.artist_id = artist_id;
                    objPlayed.track_id = track_id;
                    objPlayed.user_id = user_id;
                    objPlayed.played1 = DateTimeOffset.Now;

                    objDB.playeds.Add(objPlayed);
                }
                else
                    query.played1 = DateTime.Now;

                objDB.SaveChanges();

                return isSuccessful = true;               
            }   
            catch(Exception ex)
            {
                isSuccessful = false;
            }

            return isSuccessful;
        }

    }
}
