import { QuestionType, QuestionTemplate } from '../codegen/model.g';

/**
 * Defines data to create appropriate question component.
 */
export default interface IQuestionComponentData {
    /** Question component type. */
    component: any;

    /** Input parameters for question component. */
    inputs: {
        /** Question identifier. */
        questionId: number,
        /** Question template. */
        questionTemplate: QuestionTemplate
    };
}
