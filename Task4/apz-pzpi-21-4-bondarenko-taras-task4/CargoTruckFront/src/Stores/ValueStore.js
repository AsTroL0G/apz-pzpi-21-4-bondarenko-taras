import { makeAutoObservable } from 'mobx';
import { ValueService } from '../Api/ValueService';


class ValueStore {
  value = [];
  selectedValue = null;

  constructor() {
    makeAutoObservable(this);
  }

  async getValueBySensorId(id) {
    this.value = await ValueService.getValueBySensorId(id);
  }
}

export const valueStore = new ValueStore();
