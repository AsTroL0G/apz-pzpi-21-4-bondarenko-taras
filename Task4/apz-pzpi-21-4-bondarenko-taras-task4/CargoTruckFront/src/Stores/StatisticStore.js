import { makeAutoObservable } from 'mobx';
import { StatisticsService } from '../Api/StatisticsService';
import jwt_decode from 'jwt-decode';

class StatisticStore {
  static = null;

  constructor() {
    makeAutoObservable(this);
  }

  async getTripStatistics(id) {
    this.static = await StatisticsService.getTripStatistics(id);
  }
  async getContainerStatistics(id) {
    this.static = await StatisticsService.getContainerStatistics(id);
  }

}

export const statisticStore = new StatisticStore();

