import { makeAutoObservable } from 'mobx';
import { SensorService } from '../Api/SensorService';


class SensorStore {
  sensor = [];
  selectedSensor = null;

  constructor() {
    makeAutoObservable(this);
  }

  async getSensorByContainer(id) {
    this.sensor = await SensorService.getSensorByContainer(id);
  }
}

export const sensorStore = new SensorStore();
