using System.Collections.Generic;
using System.Linq;
using Enquete.Repository.Tradutors;
using System.Data.Entity;

namespace Enquete.Repository
{
    public class pollRepository
    {
        private enqueteEntities enqueteEntities;

        public pollRepository()
        {
            enqueteEntities = new enqueteEntities();
        }

        public DTO.poll Get(DTO.poll poll)
        {
            DTO.poll pollDTO = null;

            IQueryable<poll> query = enqueteEntities.poll;

            query = query.Where(x => x.poll_id.Equals(poll.poll_id));

            var p = query.FirstOrDefault();

            if (p != null)
            {
                p.views++;
                pollDTO = pollTradutor.ToApp(p);
                enqueteEntities.SaveChanges();
            }

            return pollDTO;
        }

        public List<DTO.poll> GetAll(DTO.poll poll)
        {
            List<DTO.poll> pollsDTO = new List<DTO.poll>();

            IQueryable<poll> query = enqueteEntities.poll;

            if (poll.poll_id > 0)
            {
                query = query.Where(x => x.poll_id.Equals(poll.poll_id));
            }

            if (!string.IsNullOrEmpty(poll.poll_description))
            {
                query = query.Where(x => x.poll_description.Contains(poll.poll_description));
            }

            foreach (var item in query)
            {
                pollsDTO.Add(pollTradutor.ToApp(item));
            }

            return pollsDTO;
        }

        public long Poll(DTO.poll poll)
        {
            var p = pollTradutor.ToBd(poll);

            enqueteEntities.Entry(p).State = EntityState.Added;

            enqueteEntities.SaveChanges();

            return p.poll_id;
        }

        public void Vote(DTO.poll poll)
        {
            var p = enqueteEntities.poll.FirstOrDefault(x => x.poll_id.Equals(poll.poll_id));

            p.option.FirstOrDefault(x => x.option_id.Equals(poll.options.FirstOrDefault().option_id)).qty++;

            enqueteEntities.SaveChanges();
        }

        public DTO.poll Stats(DTO.poll poll)
        {
            var pollDTO = new DTO.poll();

            IQueryable<poll> query = enqueteEntities.poll;

            query = query.Where(x => x.poll_id.Equals(poll.poll_id));

            if (query.FirstOrDefault() != null)
            {
                pollDTO = pollTradutor.ToApp(query.FirstOrDefault());
            }

            return pollDTO;
        }
    }
}
