import { StackFrame } from 'stacktrace-js';

/**
 * Gets string representation of stack trace.
 * @param stackFrames List of stack frames.
 */
export function getStackTrace(stackFrames: Array<StackFrame>): string {
    return stackFrames.map(frame => frame.toString()).join('\n');
}
