import { QuestionType, QuestionTemplate } from '../codegen/model.g';

export default interface IQuestionComponentData {
    component: any;
    inputs: {
        questionId: number,
        questionTemplate: QuestionTemplate
    };
}
