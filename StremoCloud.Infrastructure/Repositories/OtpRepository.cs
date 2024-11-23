//using MongoDB.Driver;
//using StremoCloud.Domain.Entities;
//using StremoCloud.Domain.Interface;
//using StremoCloud.Infrastructure.Constant;
//using StremoCloud.Infrastructure.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace StremoCloud.Infrastructure.Repositories
//{
//    public class OtpRepository : IOtpVerificationRepository
//    {
//        readonly IMongoCollection<OtpVerification> _collection;

//        //public OtpRepository(DataContext dataContext)
//        //{
//        //    _collection = dataContext
//        //        .MongoDatabase
//        //        .GetCollection<OtpVerification>(Constants.Collections.OtpVerification);
//        //}

//        //handles the verification token 
//        public async Task<OtpVerification> GetOtpByPhoneNumberOtpAsync(string phoneNumber, CancellationToken cancellationToken)
//        {
//            var filter = Builders<OtpVerification>.Filter;
//            var otpFilter = filter
//                .And(filter.Eq(d => d.PhoneNumber, phoneNumber), filter.Eq(d => d.IsDeleted, false));

//            var result = await _collection
//                .Find(otpFilter)
//                .SingleOrDefaultAsync(cancellationToken);

//            return result;
//        }
//    }
//}
