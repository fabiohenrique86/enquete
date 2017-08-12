using Enquete.DTO;

namespace Enquete.Repository.Tradutors
{
    public static class pollTradutor
    {
        public static poll ToBd(this DTO.poll pollDTO)
        {
            poll poll = null;

            if (pollDTO != null)
            {
                poll = new poll();

                poll.poll_id = pollDTO.poll_id;
                poll.poll_description = pollDTO.poll_description;
                poll.views = pollDTO.views;

                foreach (var item in pollDTO.options)
                {
                    poll.option.Add(new option()
                    {
                        option_id = item.option_id,
                        option_description = item.option_description,
                        qty = item.qty
                    });
                }
            }

            return poll;
        }

        public static DTO.poll ToApp(this poll poll)
        {
            DTO.poll pollDTO = null;

            if (poll != null)
            {
                pollDTO = new DTO.poll();

                pollDTO.poll_id = poll.poll_id;
                pollDTO.poll_description = poll.poll_description;
                pollDTO.views = poll.views;

                foreach (var item in poll.option)
                {
                    pollDTO.options.Add(new optionDTO()
                    {
                        option_id = item.option_id,
                        option_description = item.option_description,
                        qty = item.qty
                    });
                }
            }

            return pollDTO;
        }
    }
}
