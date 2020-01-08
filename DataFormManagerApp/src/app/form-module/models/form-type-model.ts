import { FormFieldsModel } from './form-fields-model';
import { Datesmodel } from './datesmodel';

export class FormTypeModel {
    FormType: string;
    FormName: string;
    EffectiveDate: Datesmodel;
    FormFields: Array<FormFieldsModel>;
}

