export class User {
    constructor(firstName, lastName, patronym, roles, phoneNumber, email, password) {
      this.firstName = firstName;
      this.lastName = lastName;
      this.patronym = patronym;
      this.roles = roles;
      this.phoneNumber = phoneNumber;
      this.email = email;
      this.password = password;
    }
  }
  
  export class UserCreate {
    constructor(firstName, lastName, patronym, phoneNumber, email, password) {
      this.firstName = firstName;
      this.lastName = lastName;
      this.patronym = patronym;
      this.phoneNumber = phoneNumber;
      this.email = email;
      this.password = password;
    }
  }
  
  export class UserDto {
    constructor(id, firstName, lastName, patronym, roles, phoneNumber, email, password) {
      this.id = id;
      this.firstName = firstName;
      this.lastName = lastName;
      this.patronym = patronym;
      this.roles = roles;
      this.phoneNumber = phoneNumber;
      this.email = email;
      this.password = password;
    }
  }
  export class Credentials {
    constructor(email, password) {
      this.email = email;
      this.password = password;
    }
  }
  
  export class UserUpdate {
    constructor(id, firstName, lastName, patronym, phoneNumber, email, password) {
      this.id = id;
      this.firstName = firstName;
      this.lastName = lastName;
      this.patronym = patronym;
      this.phoneNumber = phoneNumber;
      this.email = email;
      this.password = password;
    }
  }
  