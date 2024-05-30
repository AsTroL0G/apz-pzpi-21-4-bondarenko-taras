import { makeAutoObservable } from 'mobx';
import { NoticeService } from '../Api/NoticeService';


class NoticeStore {
  notice = [];
  selectedNotice = null;

  constructor() {
    makeAutoObservable(this);
  }

  async getNotice(id) {
    this.notice = await NoticeService.getNotice(id);
  }
}

export const noticeStore = new NoticeStore();
