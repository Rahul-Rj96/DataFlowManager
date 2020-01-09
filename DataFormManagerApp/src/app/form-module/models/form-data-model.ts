import { DataValueModel } from './data-value-model';
import { Datesmodel } from './datesmodel';

export class FormDataModel {
    FormId: number;
    FormType: string;
    FormName: string;
    FormData: Array<DataValueModel>;
    EffectiveDate: Datesmodel;
    constructor(formType: string, formName: string, formData: Array<DataValueModel>, effectiveDate: Datesmodel, formId: number = null) {
        this.FormId = formId;
        this.FormType = formType;
        this.FormName = formName;
        this.FormData = formData;
        this.EffectiveDate = effectiveDate;
    }
}
