import { EventEmitter } from '@angular/core';

import { Answer } from '../codegen/model.g';

export default interface IQuestionComponent {
    answered: EventEmitter<boolean>;
    getAnswer(): Answer;
}
