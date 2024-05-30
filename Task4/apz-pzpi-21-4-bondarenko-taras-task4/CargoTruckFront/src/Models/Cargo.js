export class Cargo {
    constructor(
      id,
      name,
      weight,
      length,
      height,
      width,
      announcedPrice,
      shippingPrice,
      noticeId,
      senderId,
      receiverId,
      citySenderId,
      addressSenderId,
      cityReceiverId,
      addressReceiverId,
      status
    ) {
      this.id = id;
      this.name = name;
      this.weight = weight;
      this.length = length;
      this.height = height;
      this.width = width;
      this.announcedPrice = announcedPrice;
      this.shippingPrice = shippingPrice;
      this.noticeId = noticeId;
      this.senderId = senderId;
      this.receiverId = receiverId;
      this.citySenderId = citySenderId;
      this.addressSenderId = addressSenderId;
      this.cityReceiverId = cityReceiverId;
      this.addressReceiverId = addressReceiverId;
      this.status = status;
    }
  }
  
  export class CargoCreateDto {
    constructor(
      name,
      weight,
      length,
      height,
      width,
      announcedPrice,
      senderId,
      receiverId,
      citySenderId,
      addressSenderId,
      cityReceiverId,
      addressReceiverId
    ) {
      this.name = name;
      this.weight = weight;
      this.length = length;
      this.height = height;
      this.width = width;
      this.announcedPrice = announcedPrice;
      this.senderId = senderId;
      this.receiverId = receiverId;
      this.citySenderId = citySenderId;
      this.addressSenderId = addressSenderId;
      this.cityReceiverId = cityReceiverId;
      this.addressReceiverId = addressReceiverId;
    }
  }
  