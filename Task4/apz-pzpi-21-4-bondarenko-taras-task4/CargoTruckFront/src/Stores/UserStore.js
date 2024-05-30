import { makeAutoObservable } from 'mobx';
import { UserService } from '../Api/UserService';
import { User, UserCreate, UserUpdate, Credentials } from './../Models/User';
import jwt_decode from 'jwt-decode';

class UserStore {
  user = null;
  users =[];
  isAuthenticated = false;

  constructor() {
    makeAutoObservable(this);
  }

  async registerUser(firstName, lastName, patronym, phoneNumber, email, password) {
    const userCreate = new UserCreate(firstName, lastName, patronym, phoneNumber, email, password);
    await UserService.registerUser(userCreate);
  }
  async fetchUserPage() {
    this.users = await UserService.fetchUser();

  }
 async loginUser(email, password) {
    const credentials = new Credentials(email, password);
    const response = await UserService.loginUser(credentials);
    if (response.status === 200) {

      const accessToken = response.data.accessToken;
      const decodedToken = jwt_decode(accessToken);
        
      const userId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
  
      this.isAuthenticated = true;
      localStorage.setItem('login', userId);

      console.log(`Logged in user ID: ${userId}`);
    }
  }

  async fetchUser(id) {
    this.user = await UserService.getUser(id);
  }
  async fetchUserByNumberPhone(numberPhone) {
    this.user = await UserService.getUserByNumberPhone(numberPhone);
  }
  async updateUser(userData) {
    const userUpdate = new UserUpdate(...userData);
    await UserService.updateUser(userUpdate);
    this.fetchUser(userUpdate.id);
  }
  async deleteUser(id) {
    await UserService.deleteUser(id);
  }
  async banUser(id) {
    await UserService.banUser(id);
  }
  async unBanUser(id) {
    await UserService.unBanUser(id);
  }
}

export const userStore = new UserStore();
