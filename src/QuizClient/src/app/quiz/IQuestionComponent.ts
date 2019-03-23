import { EventEmitter } from '@angular/core';

import { Answer } from '../codegen/model.g';
import IQuestionComponentData from './IQuestionComponentData';

/**
 * Defines question component.
 */
export default interface IQuestionComponent {

    /** Emits event when answer selected. */
    answered: EventEmitter<boolean>;

    /** Initialize question data */
    initData(data: IQuestionComponentData);

    /** Gets selected answer. */
    getAnswer(): Answer;
}
