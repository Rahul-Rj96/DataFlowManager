export class Assignformmodel {
    FormId: number;
    UserId: number;
    constructor(formId: number, userId: number) {
        this.UserId = userId;
        this.FormId = formId;
    }
}
