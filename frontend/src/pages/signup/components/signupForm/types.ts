import { InjectedFormikProps, FormikBag } from 'formik';

export interface FormProps {
    email: string
    password: string
    firstName: string
    lastName: string
    confirmPassword: string
};

export interface OwnProps {
    onSignupFormSubmit(values: FormProps): void
}

export type ComponentFormikBag = FormikBag<OwnProps, FormProps>;

export type ComponentProps = InjectedFormikProps<OwnProps, FormProps>;
