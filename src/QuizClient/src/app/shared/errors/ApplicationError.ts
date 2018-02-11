import { IServiceExceptionContract } from '../../codegen/model.g';

/**
 * Defines application exception.
 */
export default class ApplicationError implements IServiceExceptionContract {
    /** Default error code for unexpected exceptions. */
    static readonly UnexpectedErrorCode = 'UNEXPECTED';

    /** Extension properties. */
    extension: any;

    /** Internal exception object. */
    internalException: any;

    /** Error message. */
    readonly message: string;

    /**
     * Creates a new instance of {@link ApplicationError}.
     * @param message Error message.
     * @param errorCode Error code.
     */
    constructor(message?: string,
                readonly errorCode: string = ApplicationError.UnexpectedErrorCode) {
        this.message = message;
    }
}
