import { DataValueModel } from './data-value-model';
import { Datesmodel } from './datesmodel';

export class FormDataModel {
    FormId: number;
    FormType: string;
    FormData: Array<DataValueModel>;
    EffectiveDate: Datesmodel;
    constructor(formType: string, formData: Array<DataValueModel>, effectiveDate: Datesmodel, formId: number = null) {
        this.FormId = formId;
        this.FormType = formType;
        this.FormData = formData;
        this.EffectiveDate = effectiveDate;
    }
}
