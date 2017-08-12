using System;
using System.Collections.Generic;
using Enquete.DTO;
using Enquete.Repository;
using Enquete.Business.Exceptions;

namespace Enquete.Business
{
    public class pollBusiness
    {
        private pollRepository pollRepository;

        public pollBusiness()
        {
            pollRepository = new pollRepository();
        }
        
        /// <summary>
        /// Cria uma nova poll
        /// </summary>
        /// <param name="poll"></param>
        /// <returns></returns>
        public long Poll(DTO.poll poll)
        {
            try
            {
                ValidarPoll(poll);

                return pollRepository.Poll(poll);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtém uma poll
        /// </summary>
        /// <param name="poll"></param>
        /// <returns></returns>
        public DTO.poll Get(DTO.poll poll)
        {
            var pollDTO = new DTO.poll();

            try
            {
                ValidarGet(poll);

                pollDTO = pollRepository.Get(poll);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                // log
                throw ex;
            }

            return pollDTO;
        }

        /// <summary>
        /// Lista as polls
        /// </summary>
        /// <param name="poll"></param>
        /// <returns></returns>
        public List<DTO.poll> GetAll(DTO.poll poll)
        {
            List<DTO.poll> pollsDTO = null;

            try
            {
                ValidarGetAll(poll);

                pollsDTO = pollRepository.GetAll(poll);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pollsDTO;
        }

        /// <summary>
        /// Vota em uma poll
        /// </summary>
        /// <param name="poll"></param>
        public void Vote(DTO.poll poll)
        {
            try
            {
                ValidarVote(poll);

                pollRepository.Vote(poll);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Estatísticas sobre uma poll
        /// </summary>
        /// <param name="poll"></param>
        /// <returns></returns>
        public DTO.poll Stats(DTO.poll poll)
        {
            try
            {
                ValidarStats(poll);

                return pollRepository.Stats(poll);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Valida os campos obrigatórios
        /// </summary>
        /// <param name="poll"></param>
        private void ValidarPoll(DTO.poll poll)
        {
            if (poll == null)
            {
                throw new BusinessException("poll é obrigatório");
            }

            if (string.IsNullOrEmpty(poll.poll_description))
            {
                throw new BusinessException("poll_description é obrigatório");
            }

            if (poll.options == null || poll.options.Count <= 0)
            {
                throw new BusinessException("options é obrigatório");
            }
        }

        /// <summary>
        /// Valida os campos obrigatórios
        /// </summary>
        /// <param name="poll"></param>
        private void ValidarVote(DTO.poll poll)
        {
            if (poll == null)
            {
                throw new BusinessException("poll é obrigatório");
            }

            if (poll.poll_id <= 0)
            {
                throw new BusinessException("poll_id é obrigatório");
            }

            if (poll.options == null || poll.options.Count <= 0)
            {
                throw new BusinessException("options é obrigatório");
            }
        }

        /// <summary>
        /// Valida os campos obrigatórios
        /// </summary>
        /// <param name="poll"></param>
        private void ValidarStats(DTO.poll poll)
        {
            if (poll == null)
            {
                throw new BusinessException("poll é obrigatório");
            }

            if (poll.poll_id <= 0)
            {
                throw new BusinessException("poll_id é obrigatório");
            }
        }

        /// <summary>
        /// Valida os campos obrigatórios
        /// </summary>
        /// <param name="poll"></param>
        private void ValidarGet(DTO.poll poll)
        {
            if (poll == null)
            {
                throw new BusinessException("poll é obrigatório");
            }

            if (poll.poll_id <= 0)
            {
                throw new BusinessException("poll_id é obrigatório");
            }
        }

        /// <summary>
        /// Valida os campos obrigatórios
        /// </summary>
        /// <param name="poll"></param>
        private void ValidarGetAll(DTO.poll poll)
        {
            if (poll == null)
            {
                throw new BusinessException("poll é obrigatório");
            }
        }
    }
}
