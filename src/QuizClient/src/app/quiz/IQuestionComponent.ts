import { EventEmitter } from '@angular/core';

import { Answer } from '../codegen/model.g';

/**
 * Defines question component.
 */
export default interface IQuestionComponent {

    /** Emits event when answer selected. */
    answered: EventEmitter<boolean>;

    /** Gets selected answer. */
    getAnswer(): Answer;
}
