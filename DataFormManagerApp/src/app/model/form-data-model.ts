import { DataValueModel } from './data-value-model';

export class FormDataModel {
    FormType:string;
    FormData:Array<DataValueModel>;
    constructor(formType:string,formData:Array<DataValueModel>){
        this.FormType= formType;
        this.FormData=formData;
}
}