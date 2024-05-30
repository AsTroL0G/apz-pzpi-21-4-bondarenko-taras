import { ApiClient } from './ApiClient';

export const ExportService = {

    getExportEntity: async (entityName) => {
    const response = await ApiClient.get(`export/exportCVS/${entityName}`, {
        responseType: 'blob',
    });
    const url = window.URL.createObjectURL(new Blob([response.data]));
    const link = document.createElement('a');
    link.href = url;
    link.setAttribute('download', `${entityName}.csv`); // назва файлу
    document.body.appendChild(link);
    link.click();
    link.remove();
  }
};
