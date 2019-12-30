import { DataValueModel } from './data-value-model';

export class FormDataModel {
    FormId: number;
    FormType: string;
    FormData: Array<DataValueModel>;
    constructor(formType: string, formData: Array<DataValueModel>, formId: number = null) {
        this.FormId = formId;
        this.FormType = formType;
        this.FormData = formData;
    }
}
