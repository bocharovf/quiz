import { ClientExceptionContract } from '../../codegen/model.g';

/**
 * Defines application exception.
 */
export default class ApplicationError extends ClientExceptionContract {
    /** Default error code for unexpected exceptions. */
    static readonly UnexpectedErrorCode = 'UNEXPECTED';

    /** Source error object */
    source: any;

    /**
     * Creates a new instance of {@link ApplicationError}.
     * @param message Error message.
     * @param errorCode Error code.
     */
    constructor(message: string,
                errorCode: string = ApplicationError.UnexpectedErrorCode) {
        super();

        this.message = message;
        this.errorCode = errorCode;
    }
}
