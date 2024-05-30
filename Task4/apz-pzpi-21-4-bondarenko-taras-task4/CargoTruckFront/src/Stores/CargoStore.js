import { makeAutoObservable } from 'mobx';
import { CargoService } from '../Api/CargoService';
import { Cargo, CargoCreateDto } from '../Models/Cargo';
import { useNavigate,useLocation   } from "react-router-dom";

class CargoStore {
  cargos = [];
  coordinates = [];
  selectedCargo = null;

  constructor() {
    makeAutoObservable(this);
  }

  async fetchCargosBySender(id) {
    this.cargos = await CargoService.getCargoBySender(id);
  }
  async updateCargo(cargo) {
    await CargoService.updateCargo(cargo);
  }

  async fetchCargosByReceiver(id) {
    this.cargos = await CargoService.getCargoByReceiver(id);
  }

  async fetchCargo(id) {
    this.cargos = await CargoService.getCargo(id);

  }
  async fetchCargoPage() {
    this.cargos = await CargoService.fetchCargoPage();

  }
  async fetchCargoStatus() {
    this.cargos = await CargoService.fetchCargoStatus();
  }

  async updateNotice(cargoId, newNoticeId) {
    this.cargos =  await CargoService.updateNotice(cargoId,newNoticeId);
  }

  async createCargo(cargoData) {
    await CargoService.createCargo(cargoData);
  }

  async findContainer(coordinates) {
    this.coordinates = await CargoService.findContainer(coordinates.lat,coordinates.lng);
  }

  async deleteCargo(id) {
    await CargoService.deleteCargo(id);
  }




}

export const cargoStore = new CargoStore();
