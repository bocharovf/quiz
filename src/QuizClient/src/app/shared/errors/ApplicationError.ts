import { IServiceExceptionContract } from '../../codegen/model.g';

export default class ApplicationError implements IServiceExceptionContract {
    static readonly UnexpectedErrorCode = 'UNEXPECTED';

    extension: any;
    internalException: any;
    readonly message: string;

    constructor(message?: string,
                readonly errorCode: string = ApplicationError.UnexpectedErrorCode) {
        this.message = message;
    }
}
