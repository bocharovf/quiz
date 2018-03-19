/**
 * Fixes Angular Material animation issue in PhantomJS.
 * See https://github.com/angular/material2/issues/7101
 */
export function PatchDocumentStyleWithTransformProp() {
    if (!document.body.style.transform) {
        Object.defineProperty(document.body.style, 'transform', {
            value: () => {
                return {
                    enumerable: true,
                    configurable: true
                };
            },
        });
    }
}
