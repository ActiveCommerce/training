<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Account-Login-NewAccount.ascx.cs" Inherits="ActiveCommerce.Web.skins.Account_Login_NewAccount" %>

<div class="new-account" ng-controller="TrainingNewAccountCtrl"> <%-- IMPORTANT: Note new controller name  --%>
    <h3><%=Editable(x => x.FormHeader)%></h3>
    <div class="form-bg">
        <div class="inner">
            <ol id="create-account-form" class="form" ng-form="newAccountForm" ng-class="{submitted:submitted}">
                <li><label for="newaccount-email"><%=Editable(x => x.EmailLabel)%></label>
                <input type="email" class="required" id="newaccount-email" ng-model="login.email" required /></li>
                                    
                <li><label for="newaccount-password"><%=Editable(x => x.PasswordLabel)%></label>
                <input type="password" class="required" id="newaccount-password" ng-model="login.password" required /></li>
                                    
                <li><label for="newaccount-password-confirm"><%=Editable(x => x.PasswordConfirmLabel)%></label>
                <input type="password" class="required" id="newaccount-password-confirm" ng-model="login.passwordConfirm" required" /></li>
                
                <%-- ADDED: New field to form --%>
                <li><label for="newaccount-birthday">Birthday</label> <%-- TODO: Extend datasource item for this component to allow editing/i18n of label --%>
                <input type="text" class="required" id="newaccount-birthday" ng-model="login.birthday" required ac-enter="newAccount()" /></li>

                <li ng-show="errorMessage"><label class="error">{{errorMessage}}</label></li>
                
                <li class="buttons">
                    <button type="button" class="submit bttn bttn-primary bttn-block" ng-click="newAccount()" ng-disabled="loading"><%=Editable(x => x.SubmitButton)%></button>
                </li>
            </ol>
        </div>
    </div>
</div>
